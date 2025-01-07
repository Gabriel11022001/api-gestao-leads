namespace ApiGestaoLeads.Utils
{
    public class ValidaGenero
    {

        private static List<String> _generos = new List<string>()
        {
            "Masculino",
            "Feminino",
            "Outro"
        };

        public static bool ValidarGenero(string genero)
        {

            if (!_generos.Contains(genero))
            {

                return false;
            }

            return true;
        }

    }
}
