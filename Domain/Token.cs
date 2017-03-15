namespace dotnetapiapp.Domain {

    public class Token {

        public Token(string sessionId, string customerId) {
            this.SessionId = sessionId;
            this.CustomerId = customerId;
        }

        public string SessionId { get; set; }
        public string CustomerId { get; set; }
    }

}