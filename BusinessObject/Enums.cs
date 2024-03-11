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
        FinishCommission
    }
    public enum AccountStatus
    {
        Active,
        Inactive,
    }
    public enum CommissionStatus
    {
        Pending,
        Accepted,
        Denied,
        Finished,
        Canceled
    }
    public enum ImageType
    {
        JPG,
        PNG,
        GIF,
        BMP,
        TIFF,
        WEBP,
        SVG
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
