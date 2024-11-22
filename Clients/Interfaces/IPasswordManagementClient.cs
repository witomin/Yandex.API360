using System.Threading.Tasks;
using Yandex.API360.Models;

namespace Yandex.API360{
    public interface IPasswordManagementClient {
        /// <summary>
        /// Получить параметры паролей организации
        /// </summary>
        /// <returns></returns>
        public Task<PasswordParameters> GetPasswordParametersAsync();
        /// <summary>
        /// Изменить параметры паролей организации
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<PasswordParameters> EditPasswordParametersAsync(PasswordParameters parameters);
    }
}
