namespace FormApp.Models
{
    public class AddFormViewModel
    {
        public string FormName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
