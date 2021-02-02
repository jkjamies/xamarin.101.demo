using System.Collections.ObjectModel;
using System.ComponentModel;
using App1.Models;
using Xamarin.Forms;

namespace App1.ViewModels
{

    public class MainPageViewModel : INotifyPropertyChanged
    {

        public MainPageViewModel()
        {
            // Initialize observable collection of strings
            AllNotes = new ObservableCollection<NoteModel>();

            SelectedNoteCommand = new Command(async () =>
            {
                // If null selected note, return
                if (SelectedNote is null) return;

                // Set up the detail view model
                var detailViewModel = new DetailPageViewModel
                {
                    NoteText = SelectedNote.Text
                };

                // Async push to the details page
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(detailViewModel));

                // Set selected note to null after push to details page
                SelectedNote = null;
            });

            // Erase command empties the string in the input field
            EraseCommand = new Command(() => TheNote = string.Empty);

            // Save command adds the string to the AllNotes field below and
            // empties the string in the input field
            SaveCommand = new Command(() =>
            {
                // Add the note to the AllNotes below
                AllNotes.Add(new NoteModel { Text = TheNote });
                // Empty out the input field
                TheNote = string.Empty;
            });
        }

        // Observable collection of strings
        public ObservableCollection<NoteModel> AllNotes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // The note in the input field
        string theNote;
        // The accessible note w/getter & setter
        public string TheNote
        {
            get => theNote;
            set
            {
                theNote = value;

                var args = new PropertyChangedEventArgs(nameof(TheNote));

                PropertyChanged?.Invoke(this, args);
            }
        }

        // The note that is selected
        NoteModel selectedNote;
        // The accessible note that is selected w/getter & setter
        public NoteModel SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }

        // Accessible Save Command for the note
        public Command SaveCommand { get; }

        // Accessible Erase Command for the note
        public Command EraseCommand { get; }

        // Accessible Selected Note Command for the notes
        public Command SelectedNoteCommand { get; }
    }
}
