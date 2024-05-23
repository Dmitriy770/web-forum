namespace WebForum.Frontend.HttpClients.Responses;

public record Error(
    string Type,
    string Title,
    int Status,
    string Detail,
    string Instance,
    string AdditionalProp1,
    string AdditionalProp2,
    string AdditionalProp3
);