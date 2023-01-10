using System.Threading.Tasks;
using Grpc.Core;
using GrpcFilmService;

namespace Web.GrpcServices;

public class FilmServiceGrpc : FilmServiceProto.FilmServiceProtoBase
{
    public override async Task<IsSuccessResponse> DecrNotInteresting(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }
    public override async Task<IsSuccessResponse> DecrWatchedCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }
    public override async Task<IsSuccessResponse> DecrWillWatchCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse { Success = true};
    }

    public override async Task<IsSuccessResponse> IncrNotInteresting(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }
    public override async Task<IsSuccessResponse> IncrShareCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }

    public override async Task<IsSuccessResponse> IncrViewsCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }

    public override async Task<IsSuccessResponse> IncrWatchedCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }

    public override async Task<IsSuccessResponse> IncrWillWatchCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }

    public override async Task<IsSuccessResponse> Score(ScoreRequest request, Grpc.Core.ServerCallContext context)
    {
        return new IsSuccessResponse {Success = true};
    }
}