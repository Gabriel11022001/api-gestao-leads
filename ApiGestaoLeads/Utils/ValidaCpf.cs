namespace ApiGestaoLeads.Utils
{
    public class ValidaCpf
    {

        public static bool ValidarCpf(string cpf)
        {
            // Remove caracteres não numéricos
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();

            // Verifica se o CPF possui 11 dígitos
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            // Cálculo do primeiro dígito verificador
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int primeiroDigito = 11 - (soma % 11);

            if (primeiroDigito >= 10) primeiroDigito = 0;

            // Cálculo do segundo dígito verificador
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            int segundoDigito = 11 - (soma % 11);

            if (segundoDigito >= 10) segundoDigito = 0;

            // Verifica se os dígitos verificadores estão corretos
            return cpf[9] == char.Parse(primeiroDigito.ToString()) && cpf[10] == char.Parse(segundoDigito.ToString());
        }

    }
}
