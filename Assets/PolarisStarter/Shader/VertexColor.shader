// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Pinwheel/VertexColor"
{
	Properties
	{
		_Color("Tint", Color) = (1,1,1,1)
		_AmbientIntensity("Ambient intensity", Range(0,1)) = 0.1
		_IndirectIntensity("Indirect intensity", Range(0,1)) = 0.1
		_MaxFalloffDistance("Max falloff distance", Float) = 1000
		[Toggle(DISTANCE_FALLOFF)] _UseDistanceFalloff("Use distance falloff", Int) = 0
	}
		SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			Cull Back
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature DISTANCE_FALLOFF

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#pragma multi_compile_fwdbase
			#include "AutoLight.cginc"

			float4 _Color;
			float _AmbientIntensity;
			float _IndirectIntensity;
			float _MaxFalloffDistance;

			struct vInput
			{
				float4 vertex: POSITION;
				float4 vertexColor: COLOR;
				float3 localNormal: NORMAL;
			};

			struct vOutput
			{
				float4 pos: SV_POSITION;
				float3 diffuseColor: COLOR0;
				float3 ambientColor: COLOR1;
				SHADOW_COORDS(1)
			};

			vOutput vert(vInput v)
			{
				vOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPosition = mul(unity_ObjectToWorld, v.vertex);
				float distanceToCam = length(worldPosition - _WorldSpaceCameraPos);

				float3 worldNormal = UnityObjectToWorldNormal(v.localNormal);
				float NdotL = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				float lightIntensity = lerp(_IndirectIntensity, 1, NdotL);

				#if DISTANCE_FALLOFF
				float falloff = max(0, 1 - distanceToCam / _MaxFalloffDistance);
				o.diffuseColor = v.vertexColor * _Color * lightIntensity*falloff * _LightColor0.rgb;
				#else
				o.diffuseColor = v.vertexColor * _Color * lightIntensity * _LightColor0.rgb;
				#endif

				#ifdef VERTEXLIGHT_ON
				for (int index = 0; index < 4; index++)
				{
				   float4 lightPosition = float4(unity_4LightPosX0[index],
					  unity_4LightPosY0[index],
					  unity_4LightPosZ0[index], 1.0);

				   float3 vertexToLightSource =
					  lightPosition.xyz - worldPosition;
				   float3 lightDirection = normalize(vertexToLightSource);
				   float squaredDistance =
					  dot(vertexToLightSource, vertexToLightSource);
				   float attenuation = 1.0 / (1.0 +
					  unity_4LightAtten0[index] * squaredDistance);
				   float3 d = attenuation
					  * unity_LightColor[index].rgb * _Color.rgb
					  * max(0.0, dot(worldNormal, lightDirection));

				   o.diffuseColor = o.diffuseColor + d;
				}
				#endif

				o.ambientColor = ShadeSH9(half4(worldNormal,1)) * _AmbientIntensity*_Color;
				// compute shadows data
				TRANSFER_SHADOW(o)

				return o;
			}

			float4 frag(vOutput o) : COLOR
			{
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
				float shadow = SHADOW_ATTENUATION(o);
			// darken light's illumination with shadow, keep ambient intact
			float3 lighting = o.diffuseColor * shadow + o.ambientColor;
			return float4(lighting,1);
		}
		ENDCG
	}
	UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
