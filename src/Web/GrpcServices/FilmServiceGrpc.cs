using Core.Interactors;
using GrpcFilmService;

namespace Web.GrpcServices;

public class FilmServiceGrpc : FilmServiceProto.FilmServiceProtoBase
{

    readonly FilmInteractor _interactor;
    public FilmServiceGrpc(FilmInteractor interactor)
    {
        _interactor = interactor;
    }

    public override async Task<IsSuccessResponse> DecrNotInterestingCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.DecrNotInterestingCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse {Success = res };
    }
    public override async Task<IsSuccessResponse> DecrWatchedCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {   
        var res = await _interactor.DecrWatchedCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse {Success = res };
    }
    public override async Task<IsSuccessResponse> DecrWillWatchCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.DecrWillWatchCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }

    public override async Task<IsSuccessResponse> IncrNotInterestingCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.IncrNotInterestingCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }
    public override async Task<IsSuccessResponse> IncrShareCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.IncrShareCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }

    public override async Task<IsSuccessResponse> IncrViewsCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.IncrViewsCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }

    public override async Task<IsSuccessResponse> IncrWatchedCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.IncrWatchedCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }

    public override async Task<IsSuccessResponse> IncrWillWatchCount(FilmIdRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.IncrWillWatchCount(request.FilmdId, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }

    public override async Task<IsSuccessResponse> Score(ScoreRequest request, Grpc.Core.ServerCallContext context)
    {
        var res = await _interactor.AddScore(request.FilmdId, request.Score, context.CancellationToken);

        return new IsSuccessResponse { Success = res };
    }
}