using blackapi.Data;
using blackapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blackapi.Controllers
{
    // 컨트롤러를 정의하고 API 경로를 지정합니다.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // 생성자를 통해 데이터베이스 컨텍스트를 주입합니다.
        public ProductionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 전체 생산 목록을 반환합니다.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Production>>> GetProductions()
        {
            // 데이터베이스에서 모든 생산 항목을 비동기적으로 가져옵니다.
            return await _context.Productions.ToListAsync();
        }

        // 특정 ID에 해당하는 생산 항목을 반환합니다.
        [HttpGet("{id}")]
        public async Task<ActionResult<Production>> GetProduction(int id)
        {
            // 데이터베이스에서 해당 ID의 생산 항목을 비동기적으로 찾습니다.
            var production = await _context.Productions.FindAsync(id);
            // 생산 항목이 없으면 404 Not Found 응답을 반환합니다.
            if (production == null)
            {
                return NotFound();
            }
            // 생산 항목이 있으면 해당 항목을 반환합니다.
            return production;
        }

        // 새로운 생산 항목을 생성합니다.
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> PostProduction(Production production)
        {
            // 새 생산 항목을 데이터베이스에 추가합니다.
            _context.Productions.Add(production);
            // 비동기적으로 변경 사항을 저장합니다.
            await _context.SaveChangesAsync();
            // 성공 응답을 반환합니다.
            return new ApiResponse(200, "Production created successfully");
        }

        // 기존 생산 항목을 수정합니다.
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutProduction(int id, Production production)
        {
            // 요청된 ID와 생산 항목의 ID가 일치하지 않으면 잘못된 요청을 반환합니다.
            if (id != production.Id)
            {
                return BadRequest();
            }

            // 생산 항목의 상태를 수정으로 설정합니다.
            _context.Entry(production).State = EntityState.Modified;
            try
            {
                // 비동기적으로 변경 사항을 저장합니다.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // 생산 항목이 존재하지 않으면 404 Not Found 응답을 반환합니다.
                if (!ProductionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // 성공 응답을 반환합니다.
            return new ApiResponse(200, "Production updated successfully");
        }

        // 특정 ID의 생산 항목을 삭제합니다.
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteProduction(int id)
        {
            // 데이터베이스에서 해당 ID의 생산 항목을 비동기적으로 찾습니다.
            var production = await _context.Productions.FindAsync(id);
            // 생산 항목이 없으면 404 Not Found 응답을 반환합니다.
            if (production == null)
            {
                return NotFound();
            }

            // 생산 항목을 데이터베이스에서 제거합니다.
            _context.Productions.Remove(production);
            // 비동기적으로 변경 사항을 저장합니다.
            await _context.SaveChangesAsync();

            // 성공 응답을 반환합니다.
            return new ApiResponse(200, "Production deleted successfully");
        }

        // 특정 ID의 생산 항목이 존재하는지 확인합니다.
        private bool ProductionExists(int id)
        {
            // 데이터베이스에 해당 ID의 생산 항목이 있는지 확인합니다.
            return _context.Productions.Any(e => e.Id == id);
        }
    }
}
