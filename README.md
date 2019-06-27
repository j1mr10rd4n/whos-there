# WHO'S THERE?

This came from a technical assessment test set by ~~company name redacted~~. It is not intended to be a submission for that test, more of a quick look around dotnet to see how it compares to other ways I might go about implementing a solution.

## What's it about?

The assessment involves implementing 3 API endpoints to imitate a reference version given only as a swagger specification and a live reference implementation:

- fibonacci number finder
- triangle classifier (scalene, isosceles or equilateral)
- word reverser (reverses order of letters in words within a sentence)

## Approach

This version does not start with swagger code generation: my intention was to build from the ground up to get a feel for the dotnet toolset. My C#/.net chops are not fantastic right now, expect some un-idiomatic code!

Since the reference endpoints are essentially black boxes with respect to boundary conditions - each was analysed with a set of exploratory functional tests to draw out these conditions. These were translated to unit tests that expressed the discovered behaviour and an implementation of each, wrapped by its own HTTP endpoint (hmm, looks like a microservice).

## Future steps

To make this a more polished solution:

- Implement an API Gateway and service discovery (investigate (Steeltoe)[https://steeltoe.io/docs/steeltoe-discovery/])
- Add autogeneration for Swagger/OpenAPI specs
- One-click-deploy for local and remote instances
