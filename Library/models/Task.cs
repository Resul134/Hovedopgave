using System;

namespace Library.models
{
    public class Task
    {
        private int _id;
        private int _userID;
        private int _categoryID;
        private DateTime _date;
        private string _title;
        private double _price;
        private string _description;
        private string _status;
        private bool _promoted;
        private string _region;
        private DateTime _promotedEnd;
        private int _pageViews;

        public Task()
        {

        }


        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int UserId
        {
            get => _userID;
            set => _userID = value;
        }

        public int CategoryId
        {
            get => _categoryID;
            set => _categoryID = value;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public bool Promoted
        {
            get => _promoted;
            set => _promoted = value;
        }

        public string Region
        {
            get => _region;
            set => _region = value;
        }

        public DateTime PromotedEnd
        {
            get => _promotedEnd;
            set => _promotedEnd = value;
        }

        public int PageViews
        {
            get => _pageViews;
            set => _pageViews = value;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(UserId)}: {UserId}, {nameof(CategoryId)}: {CategoryId}, {nameof(Date)}: {Date}, {nameof(Title)}: {Title}, {nameof(Price)}: {Price}, {nameof(Description)}: {Description}, {nameof(Status)}: {Status}, {nameof(Promoted)}: {Promoted}, {nameof(Region)}: {Region}, {nameof(PromotedEnd)}: {PromotedEnd}, {nameof(PageViews)}: {PageViews}";
        }
    }
}