using MediatR;

public record DeleteContactCommand(Guid contactId) : IRequest;

public class DeleteContactCommandHandler(IContactRepository contactRepository) : IRequestHandler<DeleteContactCommand>
{
    public async Task Handle(DeleteContactCommand request, CancellationToken ct)
    {
        var contact = await contactRepository.GetByIdAsync(request.contactId, ct);

        if (contact is null)
        {
            throw new KeyNotFoundException();
        }

        contactRepository.Remove(contact);

        await contactRepository.SaveChangesAsync(ct);
    }
}
