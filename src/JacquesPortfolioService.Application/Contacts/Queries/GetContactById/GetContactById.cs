using AutoMapper;
using MediatR;

public record GetContactByIdQuery(Guid contactId) : IRequest<ContactDto>;

public class GetContactByIdQueryHandler( IContactRepository contactRepository, IMapper mapper)
: IRequestHandler<GetContactByIdQuery,ContactDto>
{
    public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken ct)
    {
        var contact = await contactRepository.GetByIdAsync(request.contactId,ct);

        if(contact is null)
        {
            throw new KeyNotFoundException();
        }

        return mapper.Map<ContactDto>(contact);
    }
}
