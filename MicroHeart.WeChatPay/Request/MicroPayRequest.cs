﻿using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using MicroHeart.WeChatPay.Models;
using MicroHeart.WeChatPay.Response;

namespace MicroHeart.WeChatPay.Request
{
    public class MicroPayRequest : WeChatPayRequest<MicroPayResponse>
    {
        #region 属性
        /// <summary>
        /// 设备号
        /// </summary>
        [JsonProperty("device_info")]
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        [JsonProperty("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        [JsonProperty("attach")]
        public string Attach { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [JsonProperty("total_fee")]
        public int TotalFee { get; set; }

        /// <summary>
        /// 货币类型
        /// </summary>
        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        /// <summary>
        /// 终端IP
        /// </summary>
        [JsonProperty("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// </summary>
        [JsonProperty("goods_tag")]
        public string GoodsTag { get; set; }

        /// <summary>
        /// 指定支付方式
        /// </summary>
        [JsonProperty("limit_pay")]
        public string LimitPay { get; set; }

        /// <summary>
        /// 交易起始时间
        /// </summary>
        [JsonProperty("time_start")]
        public string TimeStart { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        [JsonProperty("time_expire")]
        public string TimeExpire { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 场景信息
        /// </summary>
        [JsonProperty("scene_info")]
        public SceneInfo SceneInfo { get; set; }

        #endregion
        public override bool NeedCertificate => false;

        public override string GetApiName()
        {
            return "https://api.mch.weixin.qq.com/pay/micropay";
        }

        public override HttpMethod GetHttpMethod()
        {
            return HttpMethod.Post;
        }

        public MicroPayRequest()
        {
            SceneInfo = new SceneInfo();
        }
    }

}
