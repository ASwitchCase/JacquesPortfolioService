using AutoMapper;
using MediatR;

public record GetContactsListQuery : IRequest<List<ContactDto>>;

public class GetContactsListQueryHandler(IContactRepository contactRepository, IMapper mapper)
: IRequestHandler<GetContactsListQuery, List<ContactDto>>
{
    public async Task<List<ContactDto>> Handle(GetContactsListQuery request, CancellationToken ct)
    {
        var contacts = await contactRepository.GetAllAsync(ct);

        return mapper.Map<List<ContactDto>>(contacts);
    }
}
