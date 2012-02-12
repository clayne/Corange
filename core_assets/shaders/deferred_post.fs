uniform int width;
uniform int height;

uniform sampler2D ldr_texture;
uniform sampler2D vignetting_texture;

uniform sampler3D lut;

/* Headers */

vec3 color_correction(vec3 color, sampler3D lut, int lut_size);
vec3 fxaa(sampler2D tex, vec2 uvs, int width, int height);
vec3 fxaa_fast(sampler2D tex, vec2 uvs, int width, int height);

/* End */

void main() {
  
	vec3 color = fxaa_fast(ldr_texture, gl_TexCoord[0].xy, width, height);
    
	vec3 vignetting = texture2D(vignetting_texture, gl_TexCoord[0].xy).rgb;
	
	vignetting = mix(vignetting, vec3(1,1,1), 0.5);
  
	gl_FragColor.rgb = color_correction(color * vignetting, lut, 64);
	gl_FragColor.a = 1.0;
} 