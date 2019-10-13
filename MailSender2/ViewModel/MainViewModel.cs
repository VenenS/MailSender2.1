using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MailSender2.Classes;
using MailSender2.Services;

namespace MailSender2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        ObservableCollection<Email> _Emails;
        public ObservableCollection<Email> Emails
        {
            get { return _Emails; }
            set
            {
                if (!Set(ref _Emails, value)) return;
                _emailView = new CollectionViewSource { Source = value };
                _emailView.Filter += OnEmailsCollectionViewSourceFilter;
                RaisePropertyChanged(nameof(EmailsView));
            }
        }

        private void OnEmailsCollectionViewSourceFilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Email email) || string.IsNullOrWhiteSpace(filtName)) return;
            if (!email.Name.Contains(filtName))
                e.Accepted = false;
        }

        IDataAccessService _serviceProxy;
        private void GetEmails() => Emails = _serviceProxy.GetEmails();

        public RelayCommand ReadAllCommand { get; set; }

        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            EmailInfo = new Email();

            ReadAllCommand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
        }

        Email _EmailInfo;
        public Email EmailInfo
        {
            get { return _EmailInfo; }
            set
            {
                _EmailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        public void SaveEmail(Email email)
        {
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if (EmailInfo.Id != 0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }

        public RelayCommand<Email> SaveCommand { get; set; }

        private string filtName;
        public string FilterName
        {
            get => filtName;
            set
            {
                if (!Set(ref filtName, value)) return;
                EmailsView.Refresh();
            }
        }
        private CollectionViewSource _emailView;
        public ICollectionView EmailsView => _emailView?.View;
    }
}