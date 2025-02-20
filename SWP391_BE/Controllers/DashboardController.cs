using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using SWP391_BE.DTOs;
using Data.Models;

namespace SWP391_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardReportService _dashboardService;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardReportService dashboardService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        // CRUD Operations
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DashboardStatisticsDTO>>> GetAllReports()
        {
            try
            {
                var reports = await _dashboardService.GetAllDashboardReportsAsync();
                var reportsDto = _mapper.Map<IEnumerable<DashboardStatisticsDTO>>(reports);
                return Ok(reportsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DashboardStatisticsDTO>> GetReportById(int id)
        {
            try
            {
                var report = await _dashboardService.GetDashboardReportByIdAsync(id);
                if (report == null)
                {
                    return NotFound($"Dashboard report with ID {id} not found");
                }
                var reportDto = _mapper.Map<DashboardStatisticsDTO>(report);
                return Ok(reportDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<DashboardStatisticsDTO>> CreateReport([FromBody] DashboardStatisticsDTO reportDto)
        {
            try
            {
                var report = _mapper.Map<DashboardReport>(reportDto);
                await _dashboardService.AddDashboardReportAsync(report);
                return CreatedAtAction(nameof(GetReportById), new { id = report.ReportId }, reportDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateReport(int id, [FromBody] DashboardStatisticsDTO reportDto)
        {
            try
            {
                var existingReport = await _dashboardService.GetDashboardReportByIdAsync(id);
                if (existingReport == null)
                {
                    return NotFound($"Dashboard report with ID {id} not found");
                }

                var report = _mapper.Map<DashboardReport>(reportDto);
                report.ReportId = id;
                await _dashboardService.UpdateDashboardReportAsync(report);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeleteReport(int id)
        {
            try
            {
                var report = await _dashboardService.GetDashboardReportByIdAsync(id);
                if (report == null)
                {
                    return NotFound($"Dashboard report with ID {id} not found");
                }

                await _dashboardService.DeleteDashboardReportAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Dashboard Statistics Endpoints
        [HttpGet("statistics")]
        [AllowAnonymous]
        public async Task<ActionResult<DashboardStatisticsDTO>> GetDashboardStatistics([FromQuery] string timeRange = "Last 30 days")
        {
            try
            {
                var statistics = await _dashboardService.GetDashboardStatisticsAsync(timeRange);
                var statisticsDto = _mapper.Map<DashboardStatisticsDTO>(statistics);
                return Ok(statisticsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("history")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DashboardStatisticsDTO>>> GetDashboardHistory(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var history = await _dashboardService.GetDashboardHistoryAsync(startDate, endDate);
                var historyDto = _mapper.Map<IEnumerable<DashboardStatisticsDTO>>(history);
                return Ok(historyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sales")]
        [AllowAnonymous]
        public async Task<ActionResult<decimal>> GetTotalSales(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var totalSales = await _dashboardService.GetTotalSalesAsync(startDate, endDate);
                return Ok(totalSales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("orders")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetTotalOrders(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var totalOrders = await _dashboardService.GetTotalOrdersAsync(startDate, endDate);
                return Ok(totalOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("active-users")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetActiveUsers()
        {
            try
            {
                var activeUsers = await _dashboardService.GetActiveUsersCountAsync();
                return Ok(activeUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshDashboardStatistics([FromQuery] string timeRange = "Last 30 days")
        {
            try
            {
                await _dashboardService.UpdateDashboardStatisticsAsync(timeRange);
                return Ok(new { message = "Dashboard statistics refreshed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 