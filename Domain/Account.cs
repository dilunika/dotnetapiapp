namespace dotnetapiapp.Domain {

    public class Account {

        public Account(string accountId) {
            this.AccountId = accountId;
            this.Details = "Account details for account " + accountId;
        }

        public string AccountId { get; set; }
        public string Details { get; set; }
    }

}