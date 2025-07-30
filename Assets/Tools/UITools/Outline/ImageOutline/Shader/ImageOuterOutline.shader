// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ImageOuterOutline"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		// _Color ("Tint", Color) = (1,1,1,1)
		// [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		// [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_OutlineWidth("OutlineWidth", Range( 0 , 10)) = 1
		_AlphaValue("AlphaValue", Range( 0 , 1)) = 0.1
		_OutlineColor("OutlineColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				// float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				// fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _MainTex_ST;
			uniform float _AlphaValue;
			uniform float4 _OutlineColor;
			float4 _MainTex_TexelSize;
			uniform float _OutlineWidth;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				// OUT.color = IN.color * _Color;
				// #ifdef PIXELSNAP_ON
				// OUT.vertex = UnityPixelSnap (OUT.vertex);
				// #endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode98 = tex2D( _MainTex, uv_MainTex );
				float2 texCoord83 = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 temp_output_86_0 = (_MainTex_TexelSize).xy;
				float4 ifLocalVar96 = 0;
				if( tex2DNode98.a >= 1.0 )
				ifLocalVar96 = tex2DNode98;
				else
				ifLocalVar96 = ( step( _AlphaValue , 0.0 ) * _OutlineColor * ( tex2D( _MainTex, ( texCoord83 + ( temp_output_86_0 * _OutlineWidth * float2( 1,0 ) ) ) ).a + tex2D( _MainTex, ( texCoord83 + ( temp_output_86_0 * _OutlineWidth * float2( -1,0 ) ) ) ).a + tex2D( _MainTex, ( texCoord83 + ( temp_output_86_0 * _OutlineWidth * float2( 0,1 ) ) ) ).a + tex2D( _MainTex, ( texCoord83 + ( temp_output_86_0 * _OutlineWidth * float2( 0,-1 ) ) ) ).a ) );
				
				fixed4 c = ifLocalVar96;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;49;-327.8359,905.1927;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;53;-329.5128,1108.299;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;57;-319.435,1606.464;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;55;-319.0879,1357.517;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;58;272.488,1607.586;Inherit;True;Property;_TextureSample4;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;65;99.27284,1408.329;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;67;86.98643,1641.772;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;56;269.888,1375.318;Inherit;True;Property;_TextureSample3;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;54;277.9219,1145.256;Inherit;True;Property;_TextureSample2;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;63;88.5853,1172.862;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;52;276.2744,908.8187;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;61;85.65079,942.3555;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-483.6457,1186.326;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-485.3794,1428.992;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-493.1803,1663.859;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-481.2282,960.7598;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;90;-693.7903,1704.691;Inherit;False;Constant;_Vector3;Vector 0;2;0;Create;True;0;0;0;False;0;False;0,-1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;89;-687.293,1471.248;Inherit;False;Constant;_Vector2;Vector 0;2;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;88;-688.58,1230.526;Inherit;False;Constant;_Vector1;Vector 0;2;0;Create;True;0;0;0;False;0;False;-1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;87;-677.423,1002.269;Inherit;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;0;False;0;False;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexelSizeNode;35;-1206.515,1240.081;Inherit;False;-1;1;0;SAMPLER2D;;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;17;-1388.483,1243.852;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;86;-1007.277,1235.897;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1137.749,1447.112;Inherit;False;Property;_OutlineWidth;OutlineWidth;0;0;Create;True;0;0;0;False;0;False;1;0;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;83;-368.227,697.8818;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureTransformNode;84;-572.8578,722.744;Inherit;False;-1;False;1;0;SAMPLER2D;;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;85;-755.7308,730.118;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;94;1179.127,1196.805;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;92;1093.248,1312.187;Inherit;False;Property;_OutlineColor;OutlineColor;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;91;905.2585,1189.438;Inherit;False;Property;_AlphaValue;AlphaValue;1;0;Create;True;0;0;0;False;0;False;0.1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;1364.088,1195.438;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;96;1947.136,1106.276;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;2175.292,1108.648;Float;False;True;-1;2;ASEMaterialInspector;0;10;ImageOuterOutline;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;True;2;5;False;;10;False;;2;5;False;;10;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
Node;AmplifyShaderEditor.SamplerNode;98;1451.677,1008.992;Inherit;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;97;1265.813,1012.321;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;99;1606.38,920.162;Inherit;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;652.0558,1275.991;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
WireConnection;58;0;57;0
WireConnection;58;1;67;0
WireConnection;65;0;83;0
WireConnection;65;1;43;0
WireConnection;67;0;83;0
WireConnection;67;1;45;0
WireConnection;56;0;55;0
WireConnection;56;1;65;0
WireConnection;54;0;53;0
WireConnection;54;1;63;0
WireConnection;63;0;83;0
WireConnection;63;1;41;0
WireConnection;52;0;49;0
WireConnection;52;1;61;0
WireConnection;61;0;83;0
WireConnection;61;1;40;0
WireConnection;41;0;86;0
WireConnection;41;1;24;0
WireConnection;41;2;88;0
WireConnection;43;0;86;0
WireConnection;43;1;24;0
WireConnection;43;2;89;0
WireConnection;45;0;86;0
WireConnection;45;1;24;0
WireConnection;45;2;90;0
WireConnection;40;0;86;0
WireConnection;40;1;24;0
WireConnection;40;2;87;0
WireConnection;35;0;17;0
WireConnection;86;0;35;0
WireConnection;83;0;84;0
WireConnection;83;1;84;1
WireConnection;84;0;85;0
WireConnection;94;0;91;0
WireConnection;95;0;94;0
WireConnection;95;1;92;0
WireConnection;95;2;100;0
WireConnection;96;0;98;4
WireConnection;96;1;99;0
WireConnection;96;2;98;0
WireConnection;96;3;98;0
WireConnection;96;4;95;0
WireConnection;0;0;96;0
WireConnection;98;0;97;0
WireConnection;100;0;52;4
WireConnection;100;1;54;4
WireConnection;100;2;56;4
WireConnection;100;3;58;4
ASEEND*/
//CHKSM=674F6EE3C1540076B9BD222EF2090A0D10DCB879