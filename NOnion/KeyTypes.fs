namespace NOnion


type ExpandedBlindedPrivateKey = ExpandedBlindedPrivateKey of array<byte>

type ExpandedBlindedPublicKey =
    | ExpandedBlindedPublicKey of array<byte>

    member self.ToByteArray() =
        match self with
        | ExpandedBlindedPublicKey bytes -> bytes

type NTorOnionKey(bytes: array<byte>) =
    do
        if bytes.Length <> Constants.NTorPublicKeyLength then
            failwith "Invalid onion key length"

    member self.ToByteArray() =
        bytes

type IdentityKey =
    | IdentityKey of array<byte>

    member self.ToByteArray() =
        match self with
        | IdentityKey bytes -> bytes
