#Install SSL Certificates

Download the test certificate from [Crertificate Repository](https://github.com/thinktecture/Thinktecture.IdentityServer.v3.Samples/tree/master/source/Certificates) and prepare for the next challange.
Follow the instracution in the `readme.md` of the repository.

Use mmc.exe tool to manage installed certificates. 

Don't forget to give your current user the read permission for the certificate.

#Port binding problem

Sometime even after installing the certificate your browser will refuse the connection.

You have to check the binding is ok or not. Check if certificate hash is correctly bind to the port.

To retrive the cretificate hash use [this link](http://msdn.microsoft.com/en-us/library/ms734695(v=vs.110).aspx)


Windows has a command line tool for manupulating the bindings.

##To Display all bindings

`netsh http show sslcert | less`

##To Delete a binding

` netsh http delete sslcert ipport=0.0.0.0:44333`

##To add new binding

`netsh http add sslcert ipport=0.0.0.0:44333 certhash=6b7acc520305bfdb4f7252daeb2177cc091faae1 appid={0
0112233-4455-6677-8899-AABBCCDDEEFF}`

Sometimes the event log comes in handy to see there is a problem in the first place.
