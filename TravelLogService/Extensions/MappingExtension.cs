using TravelLogService.DTOs;
using TravelLogService.Models;

namespace TravelLogService.Extensions;

public static class MappingExtension
{
     public static TravelLog ToTravelLog(this TravelLogDto dto)
     {
         return new TravelLog()
         {
             ObservationId = dto.ObservationId,
             DistanceKm =  dto.DistanceKm,
             Mode = dto.Mode,
         };
     }   
}