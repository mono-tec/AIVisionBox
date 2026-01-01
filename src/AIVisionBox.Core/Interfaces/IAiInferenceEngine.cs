using System;
using System.Collections.Generic;
using System.Text;

namespace AIVisionBox.Core.Interfaces
{
    /// <summary>
    /// 画像処理／AI 推論エンジンの共通インターフェース。
    /// 
    /// 実装例：
    /// ・OpenCV による個数カウント
    /// ・Renesas RZ/V AI SDK を利用した推論（HTTP 経由）
    /// 
    /// UI や PLC 制御は、本インターフェースのみに依存する。
    /// </summary>
    public interface IAiInferenceEngine : IDisposable
    {
        /// <summary>
        /// エンジンを初期化する。
        /// 
        /// 引数は以下の用途を想定：
        /// ・設定ファイルパス
        /// ・モデル名
        /// ・初期化モード識別子
        /// 
        /// ※ 実際の設定読み込みはホスト側責務としてもよい。
        /// </summary>
        /// <param name="configPathOrName">設定パスまたは識別子</param>
        /// <returns>初期化成功時 true</returns>
        bool Initialize(string configPathOrName);

        /// <summary>
        /// 1 枚の画像に対して推論を実行する。
        /// 
        /// v0.x では静止画 1 枚を前提とする。
        /// 将来、動画・ストリーム対応時も
        /// 「1フレーム単位」の呼び出しを基本とする。
        /// </summary>
        /// <param name="image">入力画像データ</param>
        /// <returns>推論結果</returns>
        InferenceResult Infer(ImageData image);
    }
}
