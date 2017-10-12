using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chuyenphatnhanh.Util
{
    public class CommonUtil
    {
        public static string GetStatusDes(string status)
        {

            if (Contant.NHAN_HANG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_nhanHang;
            }

            else if (Contant.DANG_CHUYEN_HANG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_dangChuyenHang;
            }

            else if (Contant.CHUYEN_HANG_THANH_CONG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_ChuyenHangThanhCong;
            }

            else if (Contant.DANG_GIAO_HANG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_ShipHang;
            }

            else if (Contant.GIAO_HANG_THANH_CONG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_HangShiped;
            }

            else if (Contant.HUY_DON_HANG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_Cancle;
            }

            else if (Contant.CHUYEN_HANG_TRA_THANH_CONG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_ChuyenHangHuyThanhCong;
            }

            else if (Contant.DANG_CHUYEN_HANG_TRA.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_DangVanChuyenHangHuy;
            }

            else if (Contant.DANG_GIAO_HANG_TRA.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_ShipHangTraLai;
            }

            else if (Contant.TRA_THANH_CONG.Equals(status.Trim()))
            {
                return Chuyenphatnhanh.Content.Texts.RGlobal.Status_HangShipedTraLai;
            }
            else
            {
                return null;
            }
        }
    }
}