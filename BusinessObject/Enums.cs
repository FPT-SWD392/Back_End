using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public enum ArtStatus
    {
        Private,
        Public,
        Banned,
        Unavailable
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
        Active,
        Banned
    }
    public enum CommissionStatus
    {
        Pending,
        Accepted,
        Denied,
        Finished,
        Canceled
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
        User,
        Banned
    }
}
