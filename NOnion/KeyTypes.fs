namespace NOnion


type NormalEd25519PrivateKey(bytes: array<byte>) =
    do
        if bytes.Length <> Constants.Ed25519PrivateKeyLength then
            failwithf
                "Invalid private key (length=%d), %d expected"
                bytes.Length
                Constants.Ed25519PrivateKeyLength

    member self.ToByteArray() =
        bytes

type ExpandedEd25519PrivateKey(bytes: array<byte>) =
    do
        if bytes.Length <> Constants.ExpandedEd25519PrivateKeyLength then
            failwithf
                "Invalid private key (length=%d), %d expected"
                bytes.Length
                Constants.ExpandedEd25519PrivateKeyLength

    member self.ToByteArray() =
        bytes

type Ed25519PrivateKey =
    | NormalEd25519 of NormalEd25519PrivateKey
    | ExpandedEd25519 of ExpandedEd25519PrivateKey

    static member FromBytes(bytes: array<byte>) : Ed25519PrivateKey =
        if bytes.Length = Constants.ExpandedEd25519PrivateKeyLength then
            ExpandedEd25519 <| ExpandedEd25519PrivateKey bytes
        elif bytes.Length = Constants.Ed25519PrivateKeyLength then
            NormalEd25519 <| NormalEd25519PrivateKey bytes
        else
            failwithf
                "Invalid private key (length=%d), private key should either be %d (standard ed25519) or %d bytes (expanded ed25519 key)"
                bytes.Length
                Constants.Ed25519PrivateKeyLength
                Constants.ExpandedEd25519PrivateKeyLength

    member self.ToByteArray() =
        match self with
        | NormalEd25519 key -> key.ToByteArray()
        | ExpandedEd25519 key -> key.ToByteArray()

type ED25519PublicKey =
    | ED25519PublicKey of array<byte>

    member self.ToByteArray() =
        match self with
        | ED25519PublicKey bytes -> bytes

type NTorOnionKey(bytes: array<byte>) =
    do
        if bytes.Length <> Constants.NTorPublicKeyLength then
            failwithf
                "Invalid onion key length, expected %d, got %d"
                Constants.NTorPublicKeyLength
                bytes.Length

    member self.ToByteArray() =
        bytes

type IdentityKey(bytes: array<byte>) =
    do
        if bytes.Length <> Constants.IdentityKeyLength then
            failwithf
                "Invalid identity key length, expected %d, got %d"
                Constants.IdentityKeyLength
                bytes.Length

    member self.ToByteArray() =
        bytes
