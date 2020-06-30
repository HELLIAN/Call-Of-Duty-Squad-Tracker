Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class Security
    Public Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "6DG8T0ZKUM1HP1J7Y04BK51AVPT13WPJIJ1QUOMNEGOM0SEGY0
                                       32ONTP2OISA3Y01B59Z2FMKD1A48N015192B5GVI4DA7JEPRM9
                                       2VV8EGARQXPF2GL2Z93OK6IOM9RA668QHZP7XK7XF2X1C43HVE
                                       1K115FT5ZJUHVQ66I2G2G512G6VB8ZF65Y7DE6SFCV3QH1EP0N
                                       1XA1MEAQ5SLL2P502BZCMZX7UNAG61IT38KB52JSAXTST52KDM
                                       2PSKZ31HD7NEY46ROLJ9KAOL4VM5PJOOMD7C5UTXBXT9B5YCS2
                                       29ECXXL56DH13RLFU2HVCOW4I3RPWTPN1OXKQJ4G347BHYFLFJ
                                       499I5F2UQ8J3NUF31SZXLZ590DI5JABUJ0FR2XPZOP4MFFX1A6
                                       2GHCLVSR8ICLCP62ZDZPL521MJ6WDDACT6AEEZLCJGVUHUB6UD
                                       3ZMPD0B1IHRWN5BRCL6Q1OGVHNY6TSHRX3P8425TBJYPO541P8"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&HB5, &HC1, &H5F, &HE7, &H3D, &H73, &H55, &H6B, &HB4, &H87, &HAE, &HDF, &H15, &H38, &H66, &H30,
                                                                         &H71, &HC1, &H67, &H6, &H46, &H8B, &H75, &H8A, &H50})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function




    Public Function Decrypt(cipherText As String) As String
        Dim EncryptionKey As String = "6DG8T0ZKUM1HP1J7Y04BK51AVPT13WPJIJ1QUOMNEGOM0SEGY0
                                       32ONTP2OISA3Y01B59Z2FMKD1A48N015192B5GVI4DA7JEPRM9
                                       2VV8EGARQXPF2GL2Z93OK6IOM9RA668QHZP7XK7XF2X1C43HVE
                                       1K115FT5ZJUHVQ66I2G2G512G6VB8ZF65Y7DE6SFCV3QH1EP0N
                                       1XA1MEAQ5SLL2P502BZCMZX7UNAG61IT38KB52JSAXTST52KDM
                                       2PSKZ31HD7NEY46ROLJ9KAOL4VM5PJOOMD7C5UTXBXT9B5YCS2
                                       29ECXXL56DH13RLFU2HVCOW4I3RPWTPN1OXKQJ4G347BHYFLFJ
                                       499I5F2UQ8J3NUF31SZXLZ590DI5JABUJ0FR2XPZOP4MFFX1A6
                                       2GHCLVSR8ICLCP62ZDZPL521MJ6WDDACT6AEEZLCJGVUHUB6UD
                                       3ZMPD0B1IHRWN5BRCL6Q1OGVHNY6TSHRX3P8425TBJYPO541P8"
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&HB5, &HC1, &H5F, &HE7, &H3D, &H73, &H55, &H6B, &HB4, &H87, &HAE, &HDF, &H15, &H38, &H66, &H30,
                                                                         &H71, &HC1, &H67, &H6, &H46, &H8B, &H75, &H8A, &H50})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using
                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using
        Return cipherText
    End Function
End Class
