using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Senai.OpFlix.WebApi.Utils
{
    public static class HasherDeSenhas
    {

        private const int TamanhoSalt = 16;

        private const int TamanhoHash = 20;

        /// <summary>
        /// Cria um hash a partir de uma senha.
        /// </summary>
        /// <param name="senha">a senha.</param>
        /// <param name="iteracoes">numero de iterações.</param>
        /// <returns>o hash.</returns>
        public static string Hash(string senha, int iteracoes)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[TamanhoSalt]);

            var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iteracoes);
            var hash = pbkdf2.GetBytes(TamanhoHash);

            var hashBytes = new byte[TamanhoSalt + TamanhoHash];
            Array.Copy(salt, 0, hashBytes, 0, TamanhoSalt);
            Array.Copy(hash, 0, hashBytes, TamanhoSalt, TamanhoHash);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return string.Format("$MEUHASH$V1${0}${1}", iteracoes, base64Hash);
        }

        /// <summary>
        /// Cria um hash a partir de uma senha com 1000 iterações
        /// </summary>
        /// <param name="senha">a senha.</param>
        /// <returns>o hash.</returns>
        public static string Hash(string senha)
        {
            return Hash(senha, 1000);
        }

        /// <summary>
        /// Verifica se o hash é suportado.
        /// </summary>
        /// <param name="hashString">o hash.</param>
        /// <returns>se é suportado.</returns>
        public static bool HashSuportado(string hashString)
        {
            return hashString.Contains("$MEUHASH$V1$");
        }

        /// <summary>
        /// Verica uma senha com um hash.
        /// </summary>
        /// <param name="senha">a senha.</param>
        /// <param name="senhaHasheada">o hash.</param>
        /// <returns>se as senhas coincidem</returns>
        public static bool Verificar(string senha, string senhaHasheada)
        {
            if (!HashSuportado(senhaHasheada))
            {
                throw new NotSupportedException("O hash não é suportado");
            }

            var stringHashDividida = senhaHasheada.Replace("$MEUHASH$V1$", "").Split('$');
            var iterations = int.Parse(stringHashDividida[0]);
            var base64Hash = stringHashDividida[1];

            var hashBytes = Convert.FromBase64String(base64Hash);

            var salt = new byte[TamanhoSalt];
            Array.Copy(hashBytes, 0, salt, 0, TamanhoSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(TamanhoHash);

            for (var i = 0; i < TamanhoHash; i++)
            {
                if (hashBytes[i + TamanhoSalt] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
