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

    public async Task<List<TimeEntryDTO>> GetAll()
    {
        var response = await _http.GetAsync("api/timecheck");
        var message = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(message, null, response.StatusCode);

        return JsonSerializer.Deserialize<List<TimeEntryDTO>>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<TimeEntryDTO>();
    }

    public async Task<TimeEntryDTO> GetById(Guid id)
    {
        var response = await _http.GetAsync($"api/timecheck/{id}");
        var message = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(message, null, response.StatusCode);

        return JsonSerializer.Deserialize<TimeEntryDTO>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new TimeEntryDTO();
    }

    public async Task<List<TimeEntryDTO>> GetAllEntriesNoIncludes()
    {
        var response = await _http.GetAsync("api/timecheck/all-entries-no-includes");
        var message = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(message, null, response.StatusCode);

        return JsonSerializer.Deserialize<List<TimeEntryDTO>>(message, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new List<TimeEntryDTO>();
    }

    public async Task IncludeAsync(TimeCheckRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/timecheck", request);

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, response.StatusCode);
        }
    }

    public async Task<bool> UpdateTimeEntryAsync(Guid id, TimeEntryDTO dto)
    {
        var response = await _http.PutAsJsonAsync($"api/timecheck/timeentry/{dto.Id}", dto);

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(message, null, response.StatusCode);
        }

        return true;
    }

    public async Task<TimeEntryDTO> GetTodayAsync()
    {
        var localDateNow = DateOnly.FromDateTime(DateTime.UtcNow.ToLocalTime());
        var isoDate = localDateNow.ToString("yyyy-MM-dd");
        var response = await _http.GetAsync($"api/timecheck/get-by-day/{isoDate}");

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Falha ao requisitar horários marcados. Status: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content); // apagar depois de testar

        if (string.IsNullOrWhiteSpace(content))
            return new TimeEntryDTO();

        return JsonSerializer.Deserialize<TimeEntryDTO>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new TimeEntryDTO();
    }
}
