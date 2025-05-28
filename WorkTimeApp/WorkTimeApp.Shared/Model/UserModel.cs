using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTimeApp.Shared.Model
{
    public class UserModel
    {
        private long _id;
        public long Id { get=>_id; set { 
            if(Id != value)
                {
                    _id = value;
                    NotifyStateChanged();
                }
            } }
        public  string? Login { get; set; }
        public  string? Password { get; set; }
        public string? Email { get; set; }
        public string? NIP { get; set; }


        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
