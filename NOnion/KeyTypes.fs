namespace NOnion


type ExpandedBlindedPrivateKey = ExpandedBlindedPrivateKey of array<byte>

type ExpandedBlindedPublicKey =
    | ExpandedBlindedPublicKey of array<byte>

    member self.ToByteArray() =
        match self with
        | ExpandedBlindedPublicKey bytes -> bytes

type NTorOnionKey =
    | NTorOnionKey of array<byte>

    member self.ToByteArray() =
        match self with
        | NTorOnionKey bytes -> bytes

type IdentityKey =
    | IdentityKey of array<byte>

    member self.ToByteArray() =
        match self with
        | IdentityKey bytes -> bytes
