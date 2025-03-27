using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.BLL.Interfaces
{
    public interface ISkinTestResultService
    {
        Task<IEnumerable<SkinTestResultResponse>> GetAllAsync();
        Task<SkinTestResultResponse?> GetByResultIdAsync(int resultId);
        Task<SkinTestResultResponse?> GetByAnswerIdAsync(int answerId);
        Task<SkinTestResultResponse?> CreateBySkinTestAnswerIdAsync(int skinTestAnswerId, SkinTestResultRequest request);
        Task<SkinTestResultResponse?> UpdateAsync(int resultId, SkinTestResultRequest request);
        Task<bool> DeleteAsync(int resultId);
    }
}
