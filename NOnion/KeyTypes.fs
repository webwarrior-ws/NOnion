namespace NOnion


type ExpandedBlindedPrivateKey = ExpandedBlindedPrivateKey of array<byte>

type ExpandedBlindedPublicKey =
    | ExpandedBlindedPublicKey of array<byte>

    member self.ToByteArray() =
        match self with
        | ExpandedBlindedPublicKey bytes -> bytes
