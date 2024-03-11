﻿using BusinessObject;

namespace WebAPI.Model
{
    public class ViewCommissionResponse
    {
        public required int CommisionId { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime Deadline { get; set; }
        public required double Price { get; set; }
        public required CommissionStatus CommissionStatus { get; set; }
        public string? UserName { get; set; }
        public string? ArtistName { get; set; }
    }
}