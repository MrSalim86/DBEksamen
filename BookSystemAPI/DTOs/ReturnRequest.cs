﻿namespace BookSystemAPI.DTOs
{
    public class ReturnRequest
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
