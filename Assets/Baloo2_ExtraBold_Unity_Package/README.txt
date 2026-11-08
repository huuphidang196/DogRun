# Baloo 2 ExtraBold – Unity TextMeshPro package

## Cài đặt
1. Giải nén thư mục `Baloo2_ExtraBold_Unity_Package`.
2. Chép nguyên thư mục này vào thư mục `Assets` của project Unity.
3. Chờ Unity import xong và đảm bảo project đã cài TextMeshPro.
4. Trên thanh menu Unity, chọn **Tools > Fonts > Generate Baloo 2 ExtraBold TMP Asset**.
5. Font Asset được tạo tại:
   `Assets/Baloo2_ExtraBold_Unity_Package/Generated/Baloo2-ExtraBold SDF.asset`
6. Kéo asset này vào trường **Font Asset** của component TextMeshProUGUI hoặc TMP_Text.

## Thiết lập đã cấu hình
- Nguồn font: `Baloo2-ExtraBold.ttf` (weight 800).
- Atlas: 1024 x 1024.
- Render mode: SDFAA.
- Atlas Population Mode: Dynamic.
- Multi Atlas: Enabled.

Dynamic atlas giúp TMP thêm glyph khi cần, phù hợp với nội dung tiếng Việt. Nếu cần phân phối game với các ký tự đặc biệt, hãy kiểm tra glyph trong quá trình test và cân nhắc tạo một bản font asset có character set cố định.

## Lưu ý
Package này dùng Unity Editor API của TextMeshPro. Nếu Unity báo thiếu namespace hoặc kiểu `TMP_FontAsset`, hãy cài TextMeshPro qua **Window > Package Manager** (hoặc đảm bảo package TextMeshPro đã có trong project).
