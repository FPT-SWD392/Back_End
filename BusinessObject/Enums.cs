using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public enum ArtStatus
    {
        Available,
        Unavailable,
        PreOrder
    }
    public enum AcceptCommissionStatus
    {
        Open,
        Closed
    }
    public enum TransactionType
    {
        Deposite,
        Withdraw,
        Buy,
        Sell,
        RequestCommission,
        FinishCommission,
        RequestCommissionDeny,
        RequestCommissionCancel
    }
    public enum AccountStatus
    {
        Inactive,
        Active,
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
        Commission
    }
}
