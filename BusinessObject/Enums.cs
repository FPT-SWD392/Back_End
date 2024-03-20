namespace BusinessObject
{
    public enum ArtStatus
    {
        Private,
        Public,
        Banned
    }
    public enum AcceptCommissionStatus
    {
        Open,
        Closed
    }
    public enum TransactionType
    {
        DepositVnPay,
        DepositMomo,
        DepositOther,
        DepositManualAdmin,
        Withdraw,
        Buy,
        Sell,
        RequestCommission,
        FinishCommission,
        RequestCommissionDeny,
        RequestCommissionCancel,
        UpgradeToCreator,
    }
    public enum AccountStatus
    {
        Banned,
        Active
    }
    public enum CommissionStatus
    {
        Pending,
        Accepted,
        Denied,
        Finished,
        UserCanceled,
        CreatorCancelled,
        UserConfirmed,
        UserReported
    }
    public enum PostVisibility
    {
        Everyone,
        Follower,
        Hidden
    }
    public enum ReportedObjectType
    {
        Art,
        Post,
        Artist,
        Commission,
        User
    }
    public enum AdminAccountStatus
    {
        Disable,
        Enable
    }
}
