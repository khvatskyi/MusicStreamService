using System;
using System.Collections.Generic;

namespace BlazorUI
{
    public class ARException : Exception
    {
        public ARException(Dictionary<string, string[]> modelState)
            : base()
        {
            messageList = new List<string>();
            foreach (var valueList in modelState.Values)
            {

                foreach (var error in valueList)
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageList.Add(error);
                    }
                }
            }
        }

        private List<string> messageList;
        public List<string> MessageList
        {
            get
            {
                return messageList;
            }
            private set
            {
                messageList = value;
            }
        }
    }
}
