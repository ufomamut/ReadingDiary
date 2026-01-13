using ReadingDiary.Domain.Enums;

namespace ReadingDiary.Web.Extensions
{

    /// <summary>
    /// UI helper extensions for <see cref="ReadingDiaryState"/>.
    /// Provides localized (Czech) display text for reading diary states.
    /// Intended for presentation layer only.
    /// </summary>
    public static class ReadingDiaryStateExtensions
    {

        /// <summary>
        /// Converts a <see cref="ReadingDiaryState"/> value
        /// to its Czech display representation.
        /// </summary>
        public static string ToCzech(this ReadingDiaryState state)
        {
            return state switch
            {
                ReadingDiaryState.ToRead => "Ke čtení",
                ReadingDiaryState.Reading => "Čtu",
                ReadingDiaryState.Finished => "Dočteno",
                ReadingDiaryState.Postponed => "Odloženo",
                _ => state.ToString()
            };
        }
    }
}
