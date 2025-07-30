# UI工具说明文档

## 轮廓线

### 使用说明

1. 打开 Outline/ImageOutline 文件夹。
2. 选择使用外轮廓线或内轮廓线，并为目标的 Image 组件附上 ...Outline 材质。
3. 为同一个物体挂载 OutlineBehaviour 脚本，通过 OutlineBehaviour 脚本操控上述材质。

### UITools.Outline:: OutlineBehaviour

> float OutlineWidth
> 轮廓线的宽度，不会低于 0 .

> Color OutlineColor
> 轮廓线的颜色。

## 按钮动画

使用 DoTween 的 SetEase(AnimationCurve) 函数实现。

### 使用说明

1. 为目标 Button 物体挂载 ButtonClickAnim 脚本。
2. 调节组件参数。

### UITools.Animation:: ButtonClickAnim

> float AnimAmplitude
> 按钮缩放动画的强度（最大尺寸）。 

> float AnimDuration
> 按钮缩放动画的持续时间。

> AnimationCurve ScaleAnimCurve
> 按钮缩放动画的曲线，0.0 为原尺寸，1.0 为 AnimAmplitude 的尺寸。

## 移动动画

使用 DoTween 的 SetEase(AnimationCurve curve) 函数实现。

### 使用说明

1. 为目标物体挂载 DoMoveAnim 脚本。
2. 根据目标的行为设置目标的 MoveEvent 参数。
3. 若目标有多个 MoveEvent,可以使用 List 或 Diction 模式储存多个参数。

### UITools.Animation:: MoveEvent

> Transform EndPoint
> 代表运动终点的物体的 Transform 组件。 

> float MoveTime
> 移动动画的持续时间。

> AnimationCurve MoveEventAnimCurve
> 移动动画的曲线，0.0 为当前位置，1.0 为 EndPoint 的位置。

### UITools.Animation:: MoveEventMode

> MoveEventMode.Single
> 只有一个 MoveEvent 时使用的模式。

> MoveEventMode.List
> 有多个 MoveEvent，根据顺序调用时的模式。

> MoveEventMode.Dictionary
> 有多个 MoveEvent，根据字符串 ID 调用时的模式。

### UITools.Animation:: DoMoveAnim

> MoveEventMode EventMode
> 设置当前动画事件的模式。

> void PlayMoveAnim()
> 播放为 Single 模式设置的移动动画。

> void PlayMoveAnim(int index)
> 根据索引，播放为 List 模式设置的移动动画。

> void PlayMoveAnim(string key)
> 根据键，播放为 Dictionary 模式设置的移动动画。