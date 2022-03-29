using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChatAPI.ApiModels
{
    public class List
    {
        public int id { get; set; }
        public string nick { get; set; }
        public int status { get; set; }
        public int status_sub { get; set; }
        public int status_sub_sub { get; set; }
        public int time { get; set; }
        public int user_id { get; set; }
        public int sender_user_id { get; set; }
        public string hash { get; set; }
        public string ip { get; set; }
        public string referrer { get; set; }
        public int dep_id { get; set; }
        public string email { get; set; }
        public int user_status { get; set; }
        public int support_informed { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string phone { get; set; }
        public int user_typing { get; set; }
        public string user_typing_txt { get; set; }
        public int operator_typing { get; set; }
        public int has_unread_messages { get; set; }
        public int last_user_msg_time { get; set; }
        public int last_msg_id { get; set; }
        public int mail_send { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string city { get; set; }
        public string additional_data { get; set; }
        public string session_referrer { get; set; }
        public int wait_time { get; set; }
        public int chat_duration { get; set; }
        public int priority { get; set; }
        public int online_user_id { get; set; }
        public int lsync { get; set; }
        public int transfer_if_na { get; set; }
        public int transfer_timeout_ts { get; set; }
        public int transfer_timeout_ac { get; set; }
        public int transfer_uid { get; set; }
        public int pnd_time { get; set; }
        public int cls_time { get; set; }
        public int auto_responder_id { get; set; }
        public string user_tz_identifier { get; set; }
        public int na_cb_executed { get; set; }
        public int nc_cb_executed { get; set; }
        public int fbst { get; set; }
        public int operator_typing_id { get; set; }
        public int chat_initiator { get; set; }
        public string chat_variables { get; set; }
        public string remarks { get; set; }
        public string operation { get; set; }
        public string operation_admin { get; set; }
        public string screenshot_id { get; set; }
        public int unread_messages_informed { get; set; }
        public int reinform_timeout { get; set; }
        public int last_op_msg_time { get; set; }
        public int has_unread_op_messages { get; set; }
        public int unread_op_messages_informed { get; set; }
        public int user_closed_ts { get; set; }
        public int unanswered_chat { get; set; }
        public int product_id { get; set; }
        public int usaccept { get; set; }
        public string status_sub_arg { get; set; }
        public int tslasign { get; set; }
        public string chat_locale { get; set; }
        public string chat_locale_to { get; set; }
        public string uagent { get; set; }
        public int anonymized { get; set; }
        public int invitation_id { get; set; }
        public int device_type { get; set; }
        public int gbot_id { get; set; }
        public int cls_us { get; set; }
        public List<object> updateIgnoreColumns { get; set; }
    }

    public class ChatsResponse
    {
        public List<List> list { get; set; }
        public string list_count { get; set; }
        public bool error { get; set; }
    }
}
