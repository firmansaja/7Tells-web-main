namespace BusinessObjects.Surat.Models
{
    /// <summary>
    /// Represents a physical activity.
    /// </summary>
    public class SuratMasukActivity
    {
        /// <summary>
        /// The list of activity types.
        /// </summary>
        public enum ActivityType
        {
            SuratPermohonan = 1,
            SuratKeputusan = 2,
            SuratKuasa = 3,
            SuratPerintah = 4,
            SuratPengantar = 5,
            SuratEdaran = 6,
            SuratUndangan = 7,
            SuratPemberitahuan = 8
        }

        /// <summary>
        /// Constructors an activity for the provided activity type and name.
        /// </summary>
        /// <param name="activityType">The activity type for the activity.</param>
        /// <param name="name">The name for the activity.</param>
        public SuratMasukActivity(ActivityType activityType, string name = null)
        {
            Id = (int)activityType;

            // If we don't have a name argument, 
            // then use the string representation of the activity type for the name.
            Name = name ?? activityType.ToString();
        }

        /// <summary>
        /// The ID of the activity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the activity.
        /// </summary>
        public string Name { get; set; }
    


    public enum ActMediaSurat
    {
        Hardcopy = 1,
        Softcopy = 2,
        Email = 3,
    }

    /// <summary>
    /// Constructors an activity for the provided activity type and name.
    /// </summary>
    /// <param name="actMediaSurat">The activity type for the activity.</param>
    /// <param name="name">The name for the activity.</param>
    public SuratMasukActivity(ActMediaSurat actMediaSurat, string namemediasurat = null)
    {
        IdMediaSurat = (int)actMediaSurat;

        // If we don't have a name argument, 
        // then use the string representation of the activity type for the name.
        NameMediaSurat = namemediasurat ?? actMediaSurat.ToString();
    }

    /// <summary>
    /// The ID of the activity.
    /// </summary>
    public int IdMediaSurat { get; set; }

    /// <summary>
    /// The name of the activity.
    /// </summary>
    public string NameMediaSurat { get; set; }



        public enum ActJenisSurat
        {
            Internal = 1,
            Eksternal = 2,
        }

        /// <summary>
        /// Constructors an activity for the provided activity type and name.
        /// </summary>
        /// <param name="actJenisSurat">The activity type for the activity.</param>
        /// <param namejenissurat="nmejenissurat">The name for the activity.</param>
        public SuratMasukActivity(ActJenisSurat actJenisSurat, string namejenissurat = null)
        {
            IdJenisSurat = (int)actJenisSurat;

            // If we don't have a name argument, 
            // then use the string representation of the activity type for the name.
            NameJenisSurat = namejenissurat ?? actJenisSurat.ToString();
        }

        /// <summary>
        /// The ID of the activity.
        /// </summary>
        public int IdJenisSurat { get; set; }

        /// <summary>
        /// The name of the activity.
        /// </summary>
        public string NameJenisSurat { get; set; }




    }


}