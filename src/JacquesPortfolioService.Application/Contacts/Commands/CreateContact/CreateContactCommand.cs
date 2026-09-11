using AutoMapper;
using MediatR;

public record CreateContactCommand(String name, String email, String company, String message) : IRequest<ContactDto>;

public class CreateContactCommandHandler (
    IContactRepository contactRepository,
    IMapper mapper
) : IRequestHandler<CreateContactCommand, ContactDto>
{
    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken ct)
    {
        var contact = Contact.Create(request.name, request.email, request.company, request.message);
        contactRepository.Add(contact);

        await contactRepository.SaveChangesAsync(ct);

        return mapper.Map<ContactDto>(contact);
    }
}
