using IMark.Shared.Interfaces;
using IMark.Shared.Models.DTO.TimeTrackings;
using IMark.Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace IMark.Shared.Services;

public class TimeCheckService
{
    private readonly HttpClient _http;

    public TimeCheckService(HttpClient http)
    {
        _http = http;
    }

    public async Task IncludeAsync(TimeCheckRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/timecheck/include", request);

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, response.StatusCode);
        }
    }

    public async Task<TimeEntryDTO> GetTodayAsync()
    {
        var response = await _http.GetAsync("api/timecheck/today");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Falha ao requisitar horários marcados. Status: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
            return new TimeEntryDTO();

        return JsonSerializer.Deserialize<TimeEntryDTO>(content) ?? new TimeEntryDTO();
    }
}
