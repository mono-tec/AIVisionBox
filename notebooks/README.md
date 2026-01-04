# Notebooks

このフォルダは AIVisionBox における  
**画像処理アルゴリズムの検証・チューニング用 Notebook** を格納します。

## notebooks 一覧

- cards_diamond_practice.ipynb  
  OpenCV 基本操作（色変換・ROI・2値化・Morphology）の練習用

- cards_diamond_tuning.ipynb  
  cards.png を用いたダイヤ個数カウントのパラメータ調整  
  → C++ 実装（AIVisionBox.Native.OpenCv）へのフィードバック元

## 方針

- Notebook で検証
- ロジック確定後に C++ 実装へ反映
- Notebook は「正解」ではなく「思考ログ」として残す
