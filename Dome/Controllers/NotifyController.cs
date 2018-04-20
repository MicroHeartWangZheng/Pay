﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pay.Common.Util;
using Pay.WeChatPay;
using Pay.WeChatPay.Models;

namespace Dome.Controllers
{
    [Route("api/[controller]")]
    public class NotifyController : Controller
    {
        private WeChatPayOptions weChatPayOptions;

        Dictionary<string, object> resultDictionary;

        public NotifyController(WeChatPayOptions weChatPayOptions)
        {
            this.weChatPayOptions = weChatPayOptions;
        }
        
        public async Task<string> AcceptNotice(HttpRequest request)
        {


            //验签
            var body = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();

            resultDictionary = new Dictionary<string, object>();

            if (!CheckSign(body))
            {
                resultDictionary.Add("return_code", "FAIL");
                resultDictionary.Add("return_msg", "验签失败");

                return resultDictionary.ToXmlString();
            }
            var key = Tools.GetMD5(weChatPayOptions.Key).ToLower();

            var weChatPayRefundNotifyResponse = Tools.XmlToObject<WeChatPayRefundNotifyResponse>(body);

            var data = AES.Decrypt(weChatPayRefundNotifyResponse.ReqInfo, key, AESPaddingMode.PKCS7, AESCipherModeMode.ECB);

            var info = Tools.XmlToObject<EncryptedInformation>(data);
            //处理info

            resultDictionary.Add("return_code", "SUCCESS");
            return resultDictionary.ToXmlString();
        }

        public bool CheckSign(string responseXml)
        {
            var dic = Tools.XmlToDictionary(responseXml);
            if (dic["return_code"].ToString() != "SUCCESS")
            {
                return false;
            }
            var sign = dic["sign"].ToString();
            var signStr = dic.ToSortQueryParameters(false, "sign") + "&key=" + weChatPayOptions.Key;
            if (Tools.GetMD5(signStr).ToUpper() == sign)
            {
                return true;
            }
            return false;
        }
    }
}