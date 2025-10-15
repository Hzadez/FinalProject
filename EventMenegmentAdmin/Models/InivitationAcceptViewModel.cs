namespace EventMenegmentUI.Models
{
    public class InivitationAcceptViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
       public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
         public bool? IsAccepted { get; set; }
        
    }
}
