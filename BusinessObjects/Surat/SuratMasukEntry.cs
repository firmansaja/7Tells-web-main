using System;
//using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Surat
{
    /// <summary>
    /// Represents an activity that has been logged in the Fitness Frog app.
    /// </summary>
    public class SuratMasukEntry
    {
        /// <summary>
        /// The intensity level of the activity.
        /// </summary>
        public enum IntensityLevel
        {
            Tangerang,
            Cikarang,
            All
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SuratMasukEntry()
        {
        }

        /// <summary>
        /// Constructor for easily creating entries.
        /// </summary>
        /// <param name="id">The ID for the entry.</param>
        /// <param name="year">The year (1 through 9999) for the entry date.</param>
        /// <param name="month">The month (1 through 12) for the entry month.</param>
        /// <param name="day">The day (1 through the number of days for the month) for the entry day.</param>
        /// <param name="dept">The duration for the entry (in minutes).</param>
        /// <param name="nosecurity">The duration for the entry (in minutes).</param>
        /// <param name="nosurat">The duration for the entry (in minutes).</param>
        /// <param name="activityType">The activity type for the entry.</param>
        /// <param name="ditujukan">The duration for the entry (in minutes).</param>
        /// <param name="jenis">The duration for the entry (in minutes).</param>
        /// <param name="perihal">The duration for the entry (in minutes).</param>
        /// <param name="penerima">The duration for the entry (in minutes).</param>
        /// <param name="pengiriim">The duration for the entry (in minutes).</param>
        /// <param name="media surat">The duration for the entry (in minutes).</param>
        /// <param name="keterangan">The duration for the entry (in minutes).</param>
        /// <param name="unduh surat">The duration for the entry (in minutes).</param>
        public SuratMasukEntry(
            int id,
            int year, int month, int day,
            string dept,
            string nosecurity,
            string nosurat,
            SuratMasukActivity.ActivityType activityType,
            string ditujukan,
            string jenis,
            string perihal,
            string penerima,
            string pengirim,
            string mediasurat,
            string keterangan
           /// string download
       )
        {
            Id = id;
            Date = new DateTime(year, month, day);
            Dept = dept;
            NoSecurity = nosecurity;
            NoSurat = nosurat;
            ActivityId = (int)activityType;
            Perihal = perihal;
            Ditujukan = ditujukan;
            Jenis = jenis;
            Penerima = penerima;
            Pengirim = pengirim;
            MediaSurat = mediasurat;
            Keterangan = keterangan;
            ///Download = download;

        }

        /// <summary>
        /// The ID of the entry.
        /// </summary>
        [Display(Name = "No.")]
        public int Id { get; set; }

        [Display(Name = "Jenis Surat")]
        public string Jenis { get; set; }


        [Display(Name = "Unduh Format Surat")]
        public string Download { get; set; }


        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        [Display(Name = "Pengirim")]
        public string Pengirim { get; set; }

        [Display(Name = "Media Surat")]
        public string MediaSurat { get; set; }


        [Display(Name = "Penerima Surat")]
        public string Penerima { get; set; }

        [Display(Name = "Dept. Pembuat")]
        public string Dept { get; set; }

        [Display(Name = "No. Surat")]
        public string NoSurat { get; set; }

        [Display(Name = "No. From Security")]
        public string NoSecurity { get; set; }

        [Display(Name = "Tujuan Surat")]
        public string Ditujukan { get; set; }


        public string Perihal { get; set; }

        /// <summary>
        /// The date of the entry. Should not include a time portion.
        /// </summary>
        [Display(Name = "Tanggal Surat")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The activity ID for the entry. The ID value should map to an ID in the activities collection.
        /// </summary>
        [Display(Name = "Kategori Surat")]
        public int ActivityId { get; set; }


        /// <summary>
        /// The activity ID for the entry. The ID value should map to an ID in the activities collection.
        /// </summary>
        [Display(Name = "Media Surat")]
        public int IdMediaSurat { get; set; }

        /// <summary>
        /// The activity ID for the entry. The ID value should map to an ID in the activities collection.
        /// </summary>
        [Display(Name = "Jenis Surat")]
        public int IdJenisSurat { get; set; }

        /// <summary>
        /// The activity for the entry.
        /// </summary>
        public SuratMasukActivity Activity { get; set; }

        /// <summary>
        /// The duration for the entry (in minutes).
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// The level of intensity for the entry.
        /// </summary>
        [Display(Name = "Dapat Diakses Oleh Site")]
        public IntensityLevel Intensity { get; set; }

        /// <summary>
        /// Whether or not this entry should be excluded when calculating the total fitness activity.
        /// </summary>
        /// 

        [Display(Name = "Rahasia")]
        public bool Exclude { get; set; }

        /// <summary>
        /// The notes for the entry.
        /// </summary>
        /// 
        //[Required]
        //[MaxLength(200, ErrorMessage = "the Notes Field cannot be longer than 200 characters. ")]
        public string Notes { get; set; }
    }
}