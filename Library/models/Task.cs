using System;

namespace Library.models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool Promoted { get; set; }
        public string Region { get; set; }
        public DateTime PromotedEnd { get; set; }
        public int PageViews { get; set; }

        public Task()
        {

        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(UserId)}: {UserId}, {nameof(CategoryId)}: {CategoryId}, {nameof(Date)}: {Date}, {nameof(Title)}: {Title}, {nameof(Price)}: {Price}, {nameof(Description)}: {Description}, {nameof(Status)}: {Status}, {nameof(Promoted)}: {Promoted}, {nameof(Region)}: {Region}, {nameof(PromotedEnd)}: {PromotedEnd}, {nameof(PageViews)}: {PageViews}";
        }
    }
}