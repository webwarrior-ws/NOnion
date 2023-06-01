namespace NOnion


type ExpandedBlindedPrivateKey = ExpandedBlindedPrivateKey of array<byte>

type BlindedPublicKey =
    | BlindedPublicKey of array<byte>

    member self.ToByteArray() =
        match self with
        | BlindedPublicKey bytes -> bytes

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
