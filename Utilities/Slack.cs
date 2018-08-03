using Newtonsoft.Json;
using System;

namespace Utilities
{
    public static class SlackService
    {
        public static void LogErrorToSlack(string url,
            string channel,
            string username,
            string ip,
            Exception ex,
            Guid errorId,
            string controller,
            string action
            )
        {
            string message = ex.Message;
            if (ex.InnerException != null && ex.InnerException.Message != null)
                message = ex.InnerException.Message;

            SlackErrorObject seo = new SlackErrorObject()
            {
                ErrorId = errorId,
                ServerIP = ip,
                Controller = controller,
                Action = action,
                Message = message
            };

            string body = JsonConvert.SerializeObject(seo, Formatting.Indented);

            Slack slack = new Slack()
            {
                text = $"Error: {body}",
                username = username,
                icon_emoji = ":grimacing:",
                channel = $"#{channel}"
            };

            string res = url.postJSON(slack);
        }

        public static void LogToSlack(string url,
            string channel,
            string username,
            string ip,
            string controller,
            string action,
            object message)
        {
            SlackLogObject seo = new SlackLogObject()
            {
                LogId = Guid.NewGuid(),
                ServerIP = ip,
                Controller = controller,
                Action = action,
                Message = message
            };

            string body = JsonConvert.SerializeObject(seo, Formatting.Indented);

            Slack slack = new Slack()
            {
                text = $"Log Entry: {body}",
                username = username,
                icon_emoji = ":grimacing:",
                channel = $"#{channel}"
            };

            string res = url.postJSON(slack);
        }
        public class SlackErrorObject
        {
            public Guid ErrorId { get; set; }
            public string ServerIP { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Message { get; set; }
        }

        public class SlackLogObject
        {
            public Guid LogId { get; set; }
            public string ServerIP { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public object Message { get; set; }
        }
        public class Slack
        {
            public string text { get; set; }
            public string username { get; set; }
            public string icon_emoji { get; set; }
            public string channel { get; set; }

        }
    }
}
