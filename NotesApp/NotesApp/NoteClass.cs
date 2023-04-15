using System;


namespace NotesApp
{
    public class NoteClass
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Text { get; set; }

        public NoteClass(string id, string? title, DateTime? creationDate, DateTime? modifcationDate, DateTime? expirationDate, string? text)
        {
            Id = id;
            Title = title;
            CreationDate = creationDate;
            ModificationDate = modifcationDate;
            ExpirationDate = expirationDate;
            Text = text;
        }

        private NoteClass() { }
    }
        
}

