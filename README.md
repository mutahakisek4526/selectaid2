# SelectAid

SelectAid は AAC（意思伝達装置）と PC 操作支援（オーバーレイ＋マウスグリッド）を統合した WPF アプリです。視線入力・スイッチ・ジャイロ入力などの入力方式を共通の入力抽象で扱い、誤操作時に戻れる運用性を重視しています。

## ビルド

```bash
# .NET 8 SDK (Windows 10/11)
dotnet build SelectAid.sln
```

## 起動

```
SelectAid/bin/Debug/net8.0-windows/SelectAid.exe
```

## 基本操作

- **Home**: どこからでもホームへ戻ります。
- **Back**: 現在画面からホームへ戻ります。
- **Undo**: AAC の履歴を取り消します。
- **Pause**: 入力を停止/再開します（EmergencyStop を除く）。
- **Buzzer**: 即時ブザーを鳴らします。
- **PC**: PC 操作支援（Overlay/MouseGrid）画面を開きます。

## AAC（意思伝達）

1. **AAC** 画面で文字盤を選び、入力してください。
2. **Speak** を押すと SAPI で発話し履歴に記録します。
3. **Predictions** の候補は履歴頻度＋ユーザー辞書から生成されます。
4. **Phrases** 画面で定型文を追加すると即座に反映されます。

## 定型文と文字盤

- **Phrases**: 下部の入力欄から定型文を追加できます。
- **Keyboard Layouts**: レイアウトの有効/無効を切り替えできます。

## PC 操作支援（Overlay / MouseGrid）

- **Overlay Toggle**: 常時最前面の操作パネルを表示/非表示。
- **Mouse Grid**: 3x3 グリッドで範囲を絞り込み、最後にクリックします。
- **Overlay** 内のボタンは SendInput で OS にクリック/スクロール/Tab/Backspace などを送出します。

## テーマ切替

Supporter 画面から Friendly / Stylish / Kids を切り替えできます。再起動は不要です。

## バックアップと復元

- **Backup/Restore** 画面で "Create Backup" を押すと `%AppData%\SelectAid\backups` に zip を作成します。
- **Restore Selected** で選択したバックアップを復元します（復元前に退避保存を推奨）。

## ログ

`%AppData%\SelectAid\log.txt` にエラーログを出力します。

## セーフ起動

起動時に Shift キーを押し続けると SafeMode を記録し、支援者側で復旧操作を行えるようにします。

## 復旧手順（EmergencyStop / セーフ起動 / バックアップ）

1. **EmergencyStop**（F12 などに割当）で必ず Home へ戻れる設計。
2. 起動時に **Shift 長押し**で SafeMode を記録し、Supporter 画面から復旧操作。
3. **Backup/Restore** でバックアップ zip を作成し、異常時は復元。

## PC 電源操作

Supporter 画面で許可された場合のみシャットダウン/再起動/スリープ/ログオフを行います。
