namespace BrandService.Domain.ViewModels
{
    /// <summary>
    /// Retorno genérico de ação de remoção
    /// </summary>
    public class RemoveResultVm
    {
        public bool Success { get; set; }
        /// <summary>
        /// Mensagem de erro caso Success == false
        /// </summary>
        public string? Message { get; set; }
    }
}
